import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncluirContaComponent } from './incluir-conta.component';

describe('IncluirContaComponent', () => {
  let component: IncluirContaComponent;
  let fixture: ComponentFixture<IncluirContaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IncluirContaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IncluirContaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
