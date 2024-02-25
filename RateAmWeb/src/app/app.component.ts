import { TableOfRates } from './table/table.component';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Rate } from './models/rate';
import { RatesService } from './service/rates.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  
  allRates: Rate[] = [];
  constructor(private rateService: RatesService) { }
  ngOnInit(): void {
    //this.rateService.getRates().subscribe(
    //  (res: Rate[]) => {
    //  this.allRates = res;
    //},
    //  error => {
    //    console.error('Error fetching rates:', error);
    //  }
    //)
  }
 

}
