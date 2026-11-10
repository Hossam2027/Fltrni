import { Iprovider } from "./Iprovider";

export interface PendingProvidersResponse {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: Iprovider[];
}