import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarCartaoComponent } from './editar-cartao.component';

describe('EditarCartaoComponent', () => {
  let component: EditarCartaoComponent;
  let fixture: ComponentFixture<EditarCartaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditarCartaoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditarCartaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
