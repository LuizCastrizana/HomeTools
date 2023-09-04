import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReadContaVariavelDto } from 'src/app/interfaces/api-dto/Financas/readContaVariavel';
import { RespostaApi } from 'src/app/interfaces/api-dto/respostaApi';

@Injectable({
  providedIn: 'root'
})
export class ContaVariavelService {

  constructor(private http: HttpClient) { }

  private readonly ContaAPI = "https://localhost:7012/api/ContaVariavel";

  listar(): Observable<RespostaApi<ReadContaVariavelDto[]>> {
    return this.http.get<RespostaApi<ReadContaVariavelDto[]>>(this.ContaAPI);
  }

  buscar(busca: string): Observable<RespostaApi<ReadContaVariavelDto[]>> {
    const url = `${this.ContaAPI}/${busca}`
    return this.http.get<RespostaApi<ReadContaVariavelDto[]>>(url)
  }
}
