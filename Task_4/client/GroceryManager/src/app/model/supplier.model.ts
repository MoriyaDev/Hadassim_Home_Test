import { Product } from './product.model'

export interface Supplier {
  id: number;
  companyName: string;
  phoneNumber: string;
  agentName: string;
  products: Product[];
  password: string;
  role: string;

}
