import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExcluirDespesaComponent } from './excluir-despesa.component';

describe('ExcluirDespesaComponent', () => {
  let component: ExcluirDespesaComponent;
  let fixture: ComponentFixture<ExcluirDespesaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExcluirDespesaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExcluirDespesaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
