import { AfterViewInit, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Rate } from '../models/rate';
import { Currency } from '../models/currency';
import { Bank } from '../models/bank';
import { RatesService } from '../service/rates.service';
import { LiveAnnouncer } from '@angular/cdk/a11y';


@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrl: './chart.component.css',
  encapsulation: ViewEncapsulation.None
})
export class ChartComponent implements OnInit, AfterViewInit {
  public primaryXAxis?: Object;
  public chartData?: Object[];
  allRates: Rate[] = [];
  curs: Map<number, Currency> = new Map();
  constructor(private rateService: RatesService) { }


  ngOnInit(): void {

    this.rateService.getAllRates().subscribe(
      (res: Rate[]) => {
        this.allRates = res;
       /* this.chartData = this.allRates.filter(r => r.currencyId == 1).map(item => {
          return { date: item.publishDate.toString(), rate: item.buyRate };
        }) as { date: string, rate: number }[];
*/
        const uniqueDatesMap = this.allRates
          .filter(r => r.currencyId == 1)
          .reduce((acc, item) => {
            const dateString = item.publishDate.toString() as keyof typeof acc;
            if (!acc[dateString]) {
              acc[dateString] = { date: dateString, rate: item.buyRate };
            }
            return acc;
          }, {} as Record<string, { date: string, rate: number }>);

        this.chartData = Object.values(uniqueDatesMap).sort((a, b) => {
          const dateA = new Date(a.date);
          const dateB = new Date(b.date);
          return dateA.getTime() - dateB.getTime();
        });

      }
    )

    this.rateService.getBanksAndCurrencies().subscribe(
      (res: { banks: Map<number, Bank>, currencies: Map<number, Currency> }) => {
        this.curs = res.currencies
      }
    )

    this.primaryXAxis = {
      valueType: 'Category'
    };

  



  }
  ngAfterViewInit(): void {


    //  [
    //  { date: 'Jan', rate: 401 }, { date: 'Feb', rate: 280 },
    //  { date: 'Mar', rate: 402 }, { date: 'Apr', rate: 320 },
    //  { date: 'May', rate: 403 }, { date: 'Jun', rate: 320 },
    //  { date: 'Jul', rate: 405.50 }, { date: 'Aug', rate: 550 },
    //  { date: 'Sep', rate: 380 }, { date: 'Oct', rate: 300 },
    //  { date: 'Nov', rate: 250 }, { date: 'Dec', rate: 320 }
    //];

  }

}
