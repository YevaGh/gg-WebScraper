import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TableOfRates } from './table/table.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { RatesService } from './service/rates.service';
import { HttpClientModule } from '@angular/common/http';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ChartModule } from '@syncfusion/ej2-angular-charts';
import { ChartComponent } from './chart/chart.component';
import { CategoryService, LineSeriesService } from '@syncfusion/ej2-angular-charts';
import { CalculatorComponent } from './calculator/calculator.component';
import { InfoComponent } from './info/info.component';

@NgModule({
  declarations: [
    AppComponent,
    ChartComponent,
    InfoComponent,

  ],
  imports: [
    CalculatorComponent,
    BrowserModule,
    AppRoutingModule,
    TableOfRates,
    HttpClientModule,
    CommonModule,
    MatSort,
    MatSortModule,
    MatTableModule,
    ChartModule
  ],
  providers: [
    provideAnimationsAsync(),
    RatesService,
    CategoryService,
    LineSeriesService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
