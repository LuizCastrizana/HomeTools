import { Assinatura } from "./Assinatura";
import { Compra } from "./Compra";

export interface Cartao {
  Id: number;
  Nome: string;
  DiaVencimento: number;
  DiaFechamento: number;
  Assinaturas: Assinatura[];
  Compras: Compra[];
}
