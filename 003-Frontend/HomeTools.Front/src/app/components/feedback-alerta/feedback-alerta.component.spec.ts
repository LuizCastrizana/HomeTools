import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeedbackAlertaComponent } from './feedback-alerta.component';

describe('FeedbackAlertaComponent', () => {
  let component: FeedbackAlertaComponent;
  let fixture: ComponentFixture<FeedbackAlertaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FeedbackAlertaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FeedbackAlertaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
