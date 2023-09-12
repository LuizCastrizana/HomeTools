import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PainelContasComponent } from './painel-contas.component';

describe('PainelContasComponent', () => {
  let component: PainelContasComponent;
  let fixture: ComponentFixture<PainelContasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PainelContasComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PainelContasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
