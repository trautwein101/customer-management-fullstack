export interface Customer {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string | null;
    isActive: boolean;
    createdAt: string;
}

export interface CreateCustomerRequest{
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string | null;
}


export interface UpdateCustomerRequest {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string | null;
  isActive: boolean;
}