import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule ,TranslateModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

  featuredCategories = [
    {
      id: 0,
      name: 'Carpenter',
      description: 'Furniture and woodwork',
      icon: '🔨',
      theme: 'gold'
    },
    {
      id: 0,
      name: 'Plumber',
      description: 'Pipes and water repairs',
      icon: '🔧',
      theme: 'blue'
    },
    {
      id: 0,
      name: 'Electrician',
      description: 'Electrical work and repairs',
      icon: '⚡',
      theme: 'green'
    },
    {
      id: 0,
      name: 'Painter',
      description: 'Fresh looks for your space',
      icon: '🖌️',
      theme: 'rose'
    }
  ];

}