import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncluirCartaoComponent } from './incluir-cartao.component';

describe('IncluirCartaoComponent', () => {
  let component: IncluirCartaoComponent;
  let fixture: ComponentFixture<IncluirCartaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IncluirCartaoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IncluirCartaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
