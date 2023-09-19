import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExcluirCartaoComponent } from './excluir-cartao.component';

describe('ExcluirCartaoComponent', () => {
  let component: ExcluirCartaoComponent;
  let fixture: ComponentFixture<ExcluirCartaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExcluirCartaoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExcluirCartaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
