import { TableOfRates } from './table/table.component';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Rate } from './models/rate';
import { RatesService } from './service/rates.service';
import { TableRow } from './models/tableRow';
import { Currency } from './models/currency';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  
  allRates: Rate[] = [];
  selectedRow: TableRow | undefined;
  clickedCurrency: Currency | undefined;

  handleSelectedData(data: { row: TableRow, currency: Currency }): void {
    this.selectedRow = data.row;
    this.clickedCurrency = data.currency;
  }
  constructor(private rateService: RatesService) { }
  ngOnInit(): void {
  }
 

}
