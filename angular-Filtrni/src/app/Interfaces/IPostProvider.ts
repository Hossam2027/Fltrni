
export interface ICreateProvider {
  name: string;
  phone: string;
  governorate: string;
  area: string;
  address: string;
  startingPrice: number;
  latitude: number;
  longitude: number;
  categoryId: number;
  newCategoryName?: string | null;
}
