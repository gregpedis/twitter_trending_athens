import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TrendingComponent } from './components/trending/trending.component';

const routes: Routes = [
    {
        path: '',
        component: TrendingComponent,
        pathMatch: 'full'
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TrendingRoutingModule { }
