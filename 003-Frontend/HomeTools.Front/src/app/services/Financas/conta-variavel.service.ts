import { UpdateContaVariavelDto } from 'src/app/dto/financas/contas/updateContaVariavelDto';
import { CreateContaVariavelDto } from '../../dto/financas/contas/createContaVariavelDto';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReadContaVariavelDto } from 'src/app/dto/financas/contas/readContaVariavelDto';
import { RespostaApi } from 'src/app/dto/respostaApi';

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
    const url = `${this.API}/?busca=${busca}`
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

  excluir(id: string): Observable<RespostaApi<ReadContaVariavelDto>> {
    const url = `${this.API}/${id}`
    return this.http.delete<RespostaApi<ReadContaVariavelDto>>(url)
  }
}
