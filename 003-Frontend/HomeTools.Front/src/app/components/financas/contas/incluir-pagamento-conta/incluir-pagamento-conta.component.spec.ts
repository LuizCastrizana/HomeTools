import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncluirPagamentoContaComponent } from './incluir-pagamento-conta.component';

describe('IncluirPagamentoContaComponent', () => {
  let component: IncluirPagamentoContaComponent;
  let fixture: ComponentFixture<IncluirPagamentoContaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IncluirPagamentoContaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IncluirPagamentoContaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
