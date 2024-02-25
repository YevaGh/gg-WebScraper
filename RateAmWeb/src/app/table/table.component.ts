import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, ViewChild } from '@angular/core';
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
import { map } from 'rxjs';
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


  selected1: string = 'USD';
  selected2: string = 'EUR';
  selected3: string = 'RUR';
  selected4: string = 'GBP';
  selected5: string = 'GEL';

  //selectedCurrencyData: CurrencyBuySell[] = [];

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
        this.tableRows = this.ToTableRow(this.allRates, res.banks, res.currencies)
        this.dataSource = new MatTableDataSource(this.tableRows)
        this.curs = Array.from(res.currencies.values())
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
    this.dataSource.data = data.sort((a, b) => {
      return (a.bank < b.bank ? -1 : 1) * (sort.direction === 'asc' ? 1 : -1);
    })

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

        tableRowsMap.set(rate.bankId,
          {
            bankIcon: banks.get(rate.bankId)!.iconURL,
            bank: banks.get(rate.bankId)!.name,
            date: new Date(rate.publishDate),
            currencies: currenciesForEachBank
          })
      }
    })

    return Array.from(tableRowsMap.values())
  }

  getDateString(date: Date): string {
    const currentDate = new Date();
    var options: Intl.DateTimeFormatOptions

    //if (this.isSameDate(date, currentDate)) {
    //  options = { hour: '2-digit', minute: '2-digit', hour12: false };
    //} else {
    //}

    options = { year: 'numeric', month: 'short', day: '2-digit', hour: '2-digit', minute: '2-digit', hour12: false }

    const formattedDate = new Intl.DateTimeFormat('en-US', options).format(date);

    return formattedDate.trim();

  }

  getCurrency(row: TableRow, selectedCurr: string): CurrencyBuySell {
    return row.currencies.find(currency => currency.curName === selectedCurr)!
  }

  private isSameDate(rateDate: Date, todayDate: Date): boolean {
    return rateDate.getDate() === todayDate.getDate() &&
      rateDate.getMonth() === todayDate.getMonth() &&
      rateDate.getFullYear() === todayDate.getFullYear();

  }
}


