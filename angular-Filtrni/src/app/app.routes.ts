import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'serviceprovider',


        loadComponent: () => import('./serviceprovider/serviceprovider.component').then(m => m.ServiceproviderComponent)

    },
    // {
    //     path: 'addproduct',
    //     loadComponent: () => import('./addserviceproviders/addproduct.component').then(m => m.AddproductComponent)
    // },
    {
        path: 'providerdetails/:id',
        loadComponent: () => import('./providerdetails/providerdetails.component').then(m => m.ProviderdetailsComponent)
    },
    {
        path: 'login',
        loadComponent: () => import('./auth/login/login.component').then(m => m.LoginComponent)
    },
    {
        path: 'register',
        loadComponent: () => import('./auth/register/register.component').then(m => m.RegisterComponent)
    },
    {
        path: 'admin',
        loadComponent: () => import('./admin/admin.component').then(m => m.AdminComponent)
    },
    {
path: 'home',
loadComponent: () => import('./home/home.component').then(m => m.HomeComponent)
    },
    {
        path: 'providerstatus',
        loadComponent: () => import('./providerstatus/providerstatus.component').then(m => m.ProviderstatusComponent)
    } ,
    {
        path: 'addserviceprovider',
        loadComponent : () => import('./addserviceprovider/addserviceprovider.component').then(m => m.AddserviceproviderComponent)
    }




];
