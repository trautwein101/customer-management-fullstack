export interface Customer {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string | null;
    isActive: boolean;
    createdAt: string;
}