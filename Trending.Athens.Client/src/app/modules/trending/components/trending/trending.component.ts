import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Trend } from '../../models/trend';
import { TrendingService } from '../../services/trending-service/trending.service';
import { TrendDate } from '../../models/trend-date';

@Component({
  selector: 'app-trending',
  templateUrl: './trending.component.html',
  styleUrls: ['./trending.component.scss']
})
export class TrendingComponent implements OnInit, OnDestroy {

  public dates: TrendDate[] = [];
  public trends: Trend[] = [];

  public selectedDate: TrendDate;

  private trendingSub: Subscription;
  private datesSub: Subscription;

  constructor(private trendingService: TrendingService) { }

  ngOnInit(): void {
    this.trendingSub = this.trendingService.trendingObservable.subscribe(
      data => { this.trends = data; },
      error => { alert(`Error: ${error.error} Message: ${error.message}`); }
    );

    this.datesSub = this.trendingService.datesObservable.subscribe(
      data => { this.dates = data; this.selectedDate = this.dates[0]; },
      error => { alert(`Error: ${error.error} Message: ${error.message}`); }
    );

    this.trendingService.getLastTrending();
    this.trendingService.GetAvailableDates();
  }

  ngOnDestroy(): void {
    if (this.trendingSub) {
      this.trendingSub.unsubscribe();
      this.datesSub.unsubscribe();
    }
  }

  loadTrendingByDate(): void {
    this.trendingService.getTrendingByDate(this.selectedDate);
  }
}

