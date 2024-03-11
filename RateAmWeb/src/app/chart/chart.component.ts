import { AfterViewInit, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Rate } from '../models/rate';
import { Currency } from '../models/currency';
import { Bank } from '../models/bank';
import { RatesService } from '../service/rates.service';


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
  }

}
