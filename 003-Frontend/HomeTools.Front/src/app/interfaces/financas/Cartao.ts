import { Assinatura } from "./Assinatura";

export interface Cartao {
  Id: number;
  Nome: string;
  DiaVencimento: number;
  DiaFechamento: number;
  Assinaturas: Assinatura[];
}
