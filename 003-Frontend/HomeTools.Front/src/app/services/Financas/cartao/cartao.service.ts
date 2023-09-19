import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateCartaoDto } from 'src/app/dto/financas/cartoes/createCartaoDto';
import { ReadCartaoDto } from 'src/app/dto/financas/cartoes/readCartaoDto';
import { RespostaApi } from 'src/app/dto/respostaApi';

@Injectable({
  providedIn: 'root'
})
export class CartaoService {

  constructor(private http: HttpClient) { }

  private readonly API = "https://localhost:7012/api/Cartao";

  listar(): Observable<RespostaApi<ReadCartaoDto[]>> {
    return this.http.get<RespostaApi<ReadCartaoDto[]>>(this.API);
  }

  buscar(busca: string): Observable<RespostaApi<ReadCartaoDto[]>> {
    const url = `${this.API}/?busca=${busca}`
    return this.http.get<RespostaApi<ReadCartaoDto[]>>(url)
  }

  buscarPorId(id: string): Observable<RespostaApi<ReadCartaoDto>> {
    const url = `${this.API}/${id}`
    return this.http.get<RespostaApi<ReadCartaoDto>>(url)
  }

  incluir (CreateContaDto: CreateCartaoDto): Observable<RespostaApi<ReadCartaoDto>> {
    return this.http.post<RespostaApi<ReadCartaoDto>>(this.API, CreateContaDto)
  }

  atualizar (id: string, UpdateContaDto: CreateCartaoDto): Observable<RespostaApi<ReadCartaoDto>> {
    const url = `${this.API}/${id}`
    return this.http.put<RespostaApi<ReadCartaoDto>>(url, UpdateContaDto)
  }

  excluir (id: string): Observable<RespostaApi<ReadCartaoDto>> {
    const url = `${this.API}/${id}`
    return this.http.delete<RespostaApi<ReadCartaoDto>>(url)
  }
}
