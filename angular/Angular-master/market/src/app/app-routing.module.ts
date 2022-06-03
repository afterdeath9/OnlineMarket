import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoriesComponent } from './components/categories/categories.component';

import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PaymentComponent } from './components/payment/payment.component';
const routes: Routes = [

  {path :'home', component :HomeComponent},
  {path : 'categories', component :CategoriesComponent },
  {path :'login', component :LoginComponent},
  {path : 'register', component :RegisterComponent},
  {path : 'payment' , component: PaymentComponent}
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
