import { Injectable } from '@angular/core';
import { Categoria } from '../interfaces/categoria';
import { Observable } from 'rxjs';
import { RespostaApi } from '../dto/respostaApi';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService {

  constructor(private http: HttpClient) { }

  private readonly API = environment.laPlataApiAdress + "/api/Categoria";

  listar(): Observable<RespostaApi<Categoria[]>> {
    let resp = this.http.get<RespostaApi<Categoria[]>>(this.API);
    return resp;
  }

  buscar(busca: string): Observable<RespostaApi<Categoria[]>> {
    const url = `${this.API}/${busca}`
    return this.http.get<RespostaApi<Categoria[]>>(url)
  }
}
