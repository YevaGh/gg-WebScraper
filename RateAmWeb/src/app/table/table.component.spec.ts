import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableOfRates } from './table.component';

describe('TableComponent', () => {
  let component: TableOfRates;
  let fixture: ComponentFixture<TableOfRates>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TableOfRates]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TableOfRates);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
