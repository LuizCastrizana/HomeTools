import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PainelCartoesComponent } from './painel-cartoes.component';

describe('PainelCartoesComponent', () => {
  let component: PainelCartoesComponent;
  let fixture: ComponentFixture<PainelCartoesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PainelCartoesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PainelCartoesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
