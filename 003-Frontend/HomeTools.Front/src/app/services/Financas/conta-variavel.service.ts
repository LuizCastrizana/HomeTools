import { UpdateContaVariavelDto } from 'src/app/interfaces/api-dto/financas/updateContaVariavelDto';
import { CreateContaVariavelDto } from './../../interfaces/api-dto/financas/createContaVariavelDto';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReadContaVariavelDto } from 'src/app/interfaces/api-dto/financas/readContaVariavelDto';
import { RespostaApi } from 'src/app/interfaces/api-dto/respostaApi';

@Injectable({
  providedIn: 'root'
})
export class ContaVariavelService {

  constructor(private http: HttpClient) { }

  private readonly API = "https://localhost:7012/api/ContaVariavel";

  listar(): Observable<RespostaApi<ReadContaVariavelDto[]>> {
    return this.http.get<RespostaApi<ReadContaVariavelDto[]>>(this.API);
  }

  buscar(busca: string): Observable<RespostaApi<ReadContaVariavelDto[]>> {
    const url = `${this.API}/${busca}`
    return this.http.get<RespostaApi<ReadContaVariavelDto[]>>(url)
  }

  buscarPorId(id: string): Observable<RespostaApi<ReadContaVariavelDto>> {
    const url = `${this.API}/${id}`
    return this.http.get<RespostaApi<ReadContaVariavelDto>>(url)
  }

  incluir(CreateContaVariavelDto: CreateContaVariavelDto): Observable<RespostaApi<ReadContaVariavelDto>> {
    return this.http.post<RespostaApi<ReadContaVariavelDto>>(this.API, CreateContaVariavelDto)
  }

  atualizar(id: string, UpdateContaVariavelDto: UpdateContaVariavelDto): Observable<RespostaApi<ReadContaVariavelDto>> {
    const url = `${this.API}/${id}`
    return this.http.put<RespostaApi<ReadContaVariavelDto>>(url, UpdateContaVariavelDto)
  }
}
