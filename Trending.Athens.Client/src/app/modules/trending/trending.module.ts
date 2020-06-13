import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TrendingComponent } from './components/trending/trending.component';
import { TrendingRoutingModule } from './trending-routing.module';
import { TrendingService } from './services/trending-service/trending.service';


@NgModule({
  declarations: [TrendingComponent],
  providers: [TrendingService],
  imports: [CommonModule, FormsModule, TrendingRoutingModule]
})
export class TrendingModule { }
