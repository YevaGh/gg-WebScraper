import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, concatMap, map, mergeMap, of } from 'rxjs';
import { Rate } from '../models/rate';
import { Currency } from '../models/currency';
import { Bank } from '../models/bank';

@Injectable({
  providedIn: 'root'
})
export class RatesService {
  private apiUrl = 'http://localhost:5002/api/Rates';

  constructor(private httpClient: HttpClient) { }


  getBanksAndCurrencies(): Observable<{ banks: Map<number, Bank>, currencies: Map<number, Currency> }> {

    return this.httpClient.get<any>(this.apiUrl + "/banks_and_currencies/").pipe(
      map(data => this.mapToBankOrCurrency(data))
    )

  }


  getRates(): Observable<Rate[]> {

    return this.httpClient.get<any>(this.apiUrl + "/cached-rates/")
      .pipe(
        map((jsonArray: any[]) => jsonArray.map(json => this.mapJsonToRate(json)))
      );

  }
  //getRates(): Observable<Rate[]> {
  //  return this.getBanksAndCurrencies().pipe(
  //    concatMap(() => this.httpClient.get<any[]>(this.apiUrl + '/cached-rates/')),
  //    map((jsonArray: any[]) => jsonArray.map(json => this.mapJsonToRate(json)))
  //  );
  //}

  getAllRates(): Observable<Rate[]> {
    return this.httpClient.get<any>(this.apiUrl + "/all-rates/")
      .pipe(
        map((jsonArray: any[]) => jsonArray.map(json => this.mapJsonToRate(json)))
      );

  }
  private mapJsonToRate(json: any): Rate {
    return {
      publishDate: json.publishDate,
      buyRate: json.buyRate,
      sellRate: json.sellRate,
      bankId: json.bankId,
      currencyId: json.currencyId
    };
  }

  private mapToBankOrCurrency(json: any): { banks: Map<number, Bank>, currencies: Map<number, Currency> } {
    let banks: Map<number, Bank> = new Map();
    let currencies: Map<number, Currency> = new Map();

    //json.forEach((obj: any) => {
    //  if (Object.keys(obj).length == 3) {
    //    banks.set(obj.id, obj.name);
    //  } else {
    //    const currency: Currency = {
    //      name: obj.name,
    //      symbol: obj.symbol,
    //      iconURL: obj.iconURL
    //    };
    //    currencies.set(obj.id, currency);
    //  }
    //});


    for (const obj of Object.values<any>(json.currencies)) {
      const currency: Currency = {
        name: obj.name,
        symbol: obj.symbol,
        iconURL: obj.iconURL
      };
      currencies.set(obj.currencyId, currency);
    }
    for (const obj of Object.values<any>(json.banks)) {
      const bank: Bank = {
        name: obj.name,
        id: obj.bankId,
        iconURL: obj.iconURL
      };
      banks.set(obj.bankId, bank);
    }

    return { banks, currencies };
  }

}
