import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuFinancasComponent } from './menu-financas.component';

describe('MenuFinancasComponent', () => {
  let component: MenuFinancasComponent;
  let fixture: ComponentFixture<MenuFinancasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MenuFinancasComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MenuFinancasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
