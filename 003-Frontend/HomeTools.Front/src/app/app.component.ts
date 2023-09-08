import { DadosFeedbackAlerta } from './interfaces/dadosFeedbackAlerta';
import { Component, OnInit } from '@angular/core';
import { FeedbackService } from './services/feedback.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'HomeTools.Front';
  DadosFeedbackAlerta: DadosFeedbackAlerta[] = [];

  constructor(
    private feedbackService: FeedbackService
  ) { }

  ngOnInit(): void {
    FeedbackService.FeedbackAlertaEmitter.subscribe({
      next: (dadosFeedback: DadosFeedbackAlerta) => {
        this.DadosFeedbackAlerta.push(dadosFeedback);
        this.feedbackService.exibirFeedbackAlerta(dadosFeedback.Id);
      }
    });
  }
}
