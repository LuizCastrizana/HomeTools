import { TestBed } from '@angular/core/testing';

import { ContaVariavelService } from './conta-variavel.service';

describe('ContaVariavelService', () => {
  let service: ContaVariavelService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ContaVariavelService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
