import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, EventEmitter, Output, ViewChild } from '@angular/core';
import { MatSort, Sort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Rate } from '../models/rate';
import { RatesService } from '../service/rates.service';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Currency } from '../models/currency';
import { CommonModule } from '@angular/common';
import { CurrencyBuySell, TableRow } from '../models/tableRow';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { Bank } from '../models/bank';


@Component({
  selector: 'table-component',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css'],
  standalone: true,
  imports: [MatTableModule, MatSortModule, MatFormFieldModule, MatSelectModule, MatInputModule, FormsModule, CommonModule],
})
export class TableOfRates implements AfterViewInit {
  displayedColumns: string[] = ['bank', 'publishDate', 'cur1', 'cur2', 'cur3', 'cur4', 'cur5'];
  allRates: Rate[] = [];
  allBanks: Map<number, Bank> = new Map();
  curs: Currency[] = [];
  tableRows: TableRow[] = [];
  dataSource = new MatTableDataSource(this.tableRows);

  selected1!: Currency
  selected2!: Currency
  selected3!: Currency
  selected4!: Currency
  selected5!: Currency

  //selectedCurrencyData: CurrencyBuySell[] = [];

  @Output() selectedData: EventEmitter<{ row: TableRow, currency: Currency }> = new EventEmitter<{ row: TableRow, currency: Currency }>();


  hoveredCell: any;

  onMouseEnter(cell: any) {
    this.hoveredCell = cell;
  }

  onMouseLeave() {
    this.hoveredCell = null;
  }


  constructor(private _liveAnnouncer: LiveAnnouncer, private rateService: RatesService, private sanitizer: DomSanitizer) { }

  @ViewChild(MatSort)
  sort!: MatSort;

  ngOnInit(): void {
    this.rateService.getRates().subscribe(
      (res: Rate[]) => {
        this.allRates = res;
      }
    )

    this.rateService.getBanksAndCurrencies().subscribe(
      (res: { banks: Map<number, Bank>, currencies: Map<number, Currency> }) => {
        this.allBanks = res.banks;
        this.curs = Array.from(res.currencies.values())
        this.tableRows = this.ToTableRow(this.allRates, res.banks, res.currencies)
        this.dataSource = new MatTableDataSource(this.tableRows)
        this.selected1 = this.curs[0]
        this.selected2 = this.curs[1]
        this.selected3 = this.curs[2]
        this.selected4 = this.curs[3]
        this.selected5 = this.curs[4]
      }
    )
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;

  }

  announceSortChange(sort: Sort) {
    const data = this.dataSource.data.slice();

    if (!sort.active || sort.direction === '') {
      this.dataSource.data = this.tableRows;
      return;
    }
    
    switch (sort.active) {
      case 'bank':
        this.dataSource.data = data.sort((a, b) => {
      return (a.bank < b.bank ? -1 : 1) * (sort.direction === 'asc' ? 1 : -1);
        })
        break
      case 'publishDate':
        this.dataSource.data = data.sort((a, b) => {
          const dateA = new Date(a.date);
          const dateB = new Date(b.date);

          return (dateA.getTime() - dateB.getTime()) * (sort.direction === 'asc' ? 1 : -1);
        });
    }

  }

  getLowestSellValue(element: TableRow, selectedCur: string): number {
    const currencies = this.tableRows.
      flatMap(x => x.currencies).
      filter(x => x.curName == selectedCur).
      map(x => x.sell)
    return Math.min(...currencies)
  }

  getHighestBuyValue(element: TableRow, selectedCur: string): number {
    const currencies = this.tableRows.
      flatMap(x => x.currencies).
      filter(x => x.curName == selectedCur && x.buy !== 0).
      map(x => x.buy)

    return Math.max(...currencies);
  }


  private ToTableRow(rates: Rate[], banks: Map<number, Bank>, currenciesMap: Map<number, Currency>): TableRow[] {
    const tableRowsMap: Map<number, TableRow> = new Map()
    var currenciesForEachBank: CurrencyBuySell[] = []
    var cur: CurrencyBuySell

    rates.forEach(rate => {

      cur = {
        curName: currenciesMap.get(rate.currencyId)?.name!,
        buy: rate.buyRate,
        sell: rate.sellRate
      }

      if (tableRowsMap.has(rate.bankId)) {

        const obj = tableRowsMap.get(rate.bankId)!;
        obj.currencies.push(cur)

      } else {
        currenciesForEachBank = [];
        currenciesForEachBank.push(cur)
        let dateTemp = new Date(rate.publishDate)
        dateTemp.setHours(dateTemp.getHours() + 16)
        tableRowsMap.set(rate.bankId,
          {
            bankIcon: banks.get(rate.bankId)!.iconURL,
            bank: banks.get(rate.bankId)!.name,
            date: dateTemp,
           currencies: currenciesForEachBank
          })
      }
    })

    return Array.from(tableRowsMap.values())
  }

  getDateString(date: Date): string {
    var options: Intl.DateTimeFormatOptions

    options = { month: 'long', day: '2-digit', hour: '2-digit', minute: '2-digit', hour12: false }

    const formattedDate = new Intl.DateTimeFormat('en-US', options).format(date);

    return formattedDate.trim();

  }

  getCurrency(row: TableRow, selectedCurr: string): CurrencyBuySell {
    return row.currencies.find(currency => currency.curName === selectedCurr)!
  }

  emitSelectedData(selectedRow: TableRow, clickedCurrency: Currency): void {
    this.selectedData.emit({ row: selectedRow, currency: clickedCurrency });
  }


}


