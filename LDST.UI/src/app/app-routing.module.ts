import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ForbiddenComponent, NotFoundComponent } from '@ldst/organisms';
import { AuthGuard, AdminGuard } from '@ldst/shared';

const routes: Routes = [
  {
    path: 'playground-overview',
    loadChildren: () =>
      import('@ldst/playground-overview').then(
        (m) => m.PlaygroundOverviewModule
      ),
  },
  {
    path: 'playground-selector',
    loadChildren: () =>
      import('@ldst/playground-selector').then(
        (m) => m.PlaygroundSelectorModule
      ),
  },
  {
    path: 'create-playground',
    loadChildren: () =>
      import('@ldst/create-playground').then((m) => m.CreatePlaygroundModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'playground-search',
    loadChildren: () =>
      import('@ldst/playground-search').then((m) => m.PlaygroundSearchModule),
  },
  {
    path: 'privacy',
    loadChildren: () => import('@ldst/privacy').then((m) => m.PrivacyModule),
    canActivate: [AuthGuard, AdminGuard],
  },
  {
    path: 'authentication',
    loadChildren: () =>
      import('@ldst/authentication').then((m) => m.AuthenticationModule),
  },
  {
    path: 'profile',
    loadChildren: () => import('@ldst/profile').then((m) => m.ProfileModule),
    canActivate: [AuthGuard],
  },
  { path: 'forbidden', component: ForbiddenComponent },
  {
    path: '404',
    component: NotFoundComponent,
  },
  { path: '', redirectTo: '/playground-search', pathMatch: 'full' },
  { path: '**', redirectTo: '/404', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
