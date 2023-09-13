import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncluirDespesaComponent } from './incluir-despesa.component';

describe('IncluirDespesaComponent', () => {
  let component: IncluirDespesaComponent;
  let fixture: ComponentFixture<IncluirDespesaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IncluirDespesaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IncluirDespesaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
