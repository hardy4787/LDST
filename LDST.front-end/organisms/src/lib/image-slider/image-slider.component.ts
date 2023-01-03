import {
  AfterViewInit,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  ElementRef,
  OnDestroy,
  QueryList,
  Renderer2,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import KeenSlider, { KeenSliderInstance } from 'keen-slider';
import { SliderOptionParams } from './models/slider-option-params.model';
import { SportImageDetails } from './models/sport-image-details.model';
import { SPORT_IMAGES } from './services/sport-images';

@Component({
  selector: 'ldst-image-slider',
  templateUrl: './image-slider.component.html',
  styleUrls: ['./image-slider.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ImageSliderComponent implements AfterViewInit, OnDestroy {
  currentSlide = 0;
  dotHelper = Array.from(
    { length: SPORT_IMAGES.length },
    (_, i) => i
  ) as number[];

  @ViewChild('sliderRef') sliderRef!: ElementRef<HTMLElement>;

  @ViewChildren('slideDots')
  slideDots!: QueryList<ElementRef>;

  slider!: KeenSliderInstance;
  readonly SPORT_IMAGES = SPORT_IMAGES;

  constructor(
    private render: Renderer2,
    private readonly changeDetectorRef: ChangeDetectorRef
  ) {}

  ngAfterViewInit(): void {
    this.slider = new KeenSlider(
      this.sliderRef.nativeElement,
      {
        loop: true,
        breakpoints: {
          '(min-width: 1000px)': {
            slides: { perView: 2 },
          },
          '(min-width: 1500px)': {
            slides: { perView: 3 },
          },
        },
        slides: { perView: 1 },
        initial: this.currentSlide,
        slideChanged: ({ track: { details } }) => {
          this.currentSlide = details.rel;
          this.changeDetectorRef.markForCheck();
        },
        optionsChanged: ({ options }) => {
          this.dotHelper = [
            ...Array(
              SPORT_IMAGES.length -
                (options.slides as SliderOptionParams).perView +
                1
            ).keys(),
          ];
        },
      },
      [
        (slider) => {
          let timeout: NodeJS.Timeout;
          let mouseOver = false;
          const clearNextTimeout = () => clearTimeout(timeout);
          const nextTimeout = () => {
            clearTimeout(timeout);
            if (mouseOver) return;
            timeout = setTimeout(() => {
              slider.next();
            }, 10000);
          };
          slider.on('created', () => {
            slider.container.addEventListener('mouseover', () => {
              mouseOver = true;
              clearNextTimeout();
            });
            slider.container.addEventListener('mouseout', () => {
              mouseOver = false;
              nextTimeout();
            });
            nextTimeout();
          });
          slider.on('dragStarted', clearNextTimeout);
          slider.on('animationEnded', nextTimeout);
          slider.on('updated', nextTimeout);
        },
      ]
    );

    const currentSlideDotElement = this.slideDots.find(
      (_, index) => index === this.currentSlide
    );
    this.render.addClass(currentSlideDotElement?.nativeElement, 'active');
  }

  ngOnDestroy(): void {
    if (this.slider) this.slider.destroy();
  }

  imageDetailsTrackBy = (_: number, details: SportImageDetails): number =>
    details.id;
}
