import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PainelDespesasComponent } from './painel-despesas.component';

describe('PainelDespesasComponent', () => {
  let component: PainelDespesasComponent;
  let fixture: ComponentFixture<PainelDespesasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PainelDespesasComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PainelDespesasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
