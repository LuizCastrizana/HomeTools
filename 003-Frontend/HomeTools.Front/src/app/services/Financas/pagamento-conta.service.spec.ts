import { TestBed } from '@angular/core/testing';

import { PagamentoContaService } from './pagamento-conta.service';

describe('PagamentoContaService', () => {
  let service: PagamentoContaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PagamentoContaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
