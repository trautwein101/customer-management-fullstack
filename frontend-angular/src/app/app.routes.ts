import { Routes } from '@angular/router';
import { CustomerList } from './features/customers/customer-list/customer-list';
import { CustomerCreate } from './features/customers/customer-create/customer-create';
import { CustomerEdit } from './features/customers/customer-edit/customer-edit';
import { Login } from './features/auth/login/login';
import { authGuard } from './core/auth/guards/auth.guard';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full'
    },
    {
        path: 'login',
        component: Login
    },
    {
        path: 'customers',
        component: CustomerList,
        canActivate: [authGuard]
    },
    {
        path: 'customers/create',
        component: CustomerCreate,
        canActivate: [authGuard]
    },
    {
        path: 'customers/:id/edit',
        component: CustomerEdit,
        canActivate: [authGuard]
    }
];
