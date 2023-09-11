import { TestBed } from '@angular/core/testing';

import { PagamentoContaVariavelService } from './pagamento-conta-variavel.service';

describe('PagamentoContaVariavelService', () => {
  let service: PagamentoContaVariavelService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PagamentoContaVariavelService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
