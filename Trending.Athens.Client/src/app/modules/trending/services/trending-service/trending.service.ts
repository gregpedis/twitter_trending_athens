import { Trend } from '../../models/trend';
import { Subject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TrendDate } from '../../models/trend-date';

@Injectable()
export class TrendingService {

  private trendingDataSource = new Subject<Trend[]>();
  public trendingObservable = this.trendingDataSource.asObservable();

  private datesDataSource = new Subject<TrendDate[]>();
  public datesObservable = this.datesDataSource.asObservable();

  constructor(private http: HttpClient) { }

  public getTrendingByDate(date: TrendDate) {
    this.http
      .get<Trend[]>(`/api/trending/data/${date.year}/${date.month}/${date.day}`)
      .subscribe(
        data => { this.trendingDataSource.next(data); },
        error => { this.trendingDataSource.error(error); }
      );
  }

  public getLastTrending() {
    this.http
      .get<Trend[]>('/api/trending/data/last')
      .subscribe(
        data => { this.trendingDataSource.next(data); },
        error => { this.trendingDataSource.error(error); }
      );
  }

  public GetAvailableDates() {
    this.http
      .get<TrendDate[]>('/api/trending/dates')
      .subscribe(
        data => { this.datesDataSource.next(data); },
        error => { this.datesDataSource.error(error); }
      );
  }
}
