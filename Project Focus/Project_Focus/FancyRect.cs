using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Focus {
	public class FancyRect
	{
		public float x;
		public float y;
		public float width;
		public float height;

		public FancyRect(float x, float y, float width, float height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		public Rectangle toFlashRect() {
			return new Rectangle((int)this.x, (int)this.y, (int)this.width, (int)this.height);
		}

		public string toString() {
			return "FancyRect[x1: " + this.x + ", y1: " + this.y + ", x2: " + (this.x + this.width) + ", y2: " + (this.y + this.height) + "]";
		}

		//Check if this overlaps another rect
		public bool overlaps(FancyRect other) {
			return FancyRect.Overlaps(this, other);
		}
		//Check if rect1 and rect2 overlap
		public static bool Overlaps(FancyRect rect1, FancyRect rect2) {
				
				// This is harder to read, but I think more performant than constructing a bunch of vectors and projecting
				return rect1.x + rect1.width > rect2.x 
					&& rect1.x < rect2.x + rect2.width 
				    && rect1.y + rect1.height > rect2.y
					&& rect1.y < rect2.y + rect2.height;
		}
		public FancyRect clip(FancyRect other) { return FancyRect.Clip(this, other); }

		public bool collidePoint(Point point) {
			if (point.X >= this.x && point.X <= this.x + this.width
				&& point.Y >= this.y && point.Y <= this.y + this.height) {
					return true;
				}
			return false;
		}

		/**
		 * Return the portion of rect1 contained in rect2 - a.k.a. rect1 ∩ rect2
		 * @param	rect1
		 * @param	rect2
		 * @return
		 */
		public static FancyRect Clip(FancyRect rect1, FancyRect rect2) {
			if (rect1.bottom <= rect2.top || rect1.top >= rect2.bottom || rect1.right <= rect2.left || rect1.left >= rect2.right) { return new FancyRect(0, 0, 0, 0); }
			float clipTop;
			float clipLeft;
			float clipBottom;
			float clipRight;
			clipTop = (rect1.top > rect2.top ? rect1.top : rect2.top);
			clipLeft = (rect1.left > rect2.left ? rect1.left : rect2.left);
			clipBottom = (rect1.bottom < rect2.bottom ? rect1.bottom : rect2.bottom);
			clipRight = (rect1.right < rect2.right ? rect1.right : rect2.right);
			return new FancyRect(clipLeft, clipTop, (clipRight - clipLeft), (clipBottom - clipTop));
		}

		/**
		 * Take this and compare with other to decide if they overlap. Then move this out of collision accordingly. Return the resultant this;
		 */
		public FancyRect collideRect(FancyRect other) {
			return FancyRect.CollideRect(this, other);
		}

		/**
		 * Take rect1 and compare with rect2 to decide if they overlap. Then move rect1 out of collision accordingly. Return the resultant rect1;
		 */
		public static FancyRect CollideRect(FancyRect rect1, FancyRect rect2) {
			//FancyRect resultant = FancyRect.Copy(rect1);
			FancyRect resultant = rect1;
			FancyRect overlap = FancyRect.Clip(rect1, rect2);
			if (overlap.width > 0 && overlap.height > 0){
				if (overlap.width < overlap.height) {
					if (rect1.right > rect2.left && rect1.right < rect2.left + rect2.width) { resultant.right = rect2.left; }
					else if (rect1.left < rect2.right && rect1.left > rect2.right - rect2.width) { resultant.left = rect2.right; }
				}
				else {
					if (rect1.bottom > rect2.top && rect1.bottom < rect2.top + rect2.height) { resultant.bottom = rect2.top; }
					else if(rect1.top < rect2.bottom && rect1.top > rect2.bottom - rect2.height) { resultant.top = rect2.bottom; }
				}
			}
			return resultant;
		}

		/**
		 * Utility function for player collisions to get a list of the resultant rectangles from a collision between two other rectangles. Subtract A from B, returning a set of the rectangles that B is split into
		 * @param	A
		 * @param	B
		 * @return
		 */
		public static List<FancyRect> SubtractRect(FancyRect A, FancyRect B) {
			FancyRect U = new FancyRect(Math.Min(A.left, B.left), Math.Min(A.top, B.top), Math.Max(A.right, B.right) - Math.Min(A.left, B.left), Math.Max(A.bottom, B.bottom) - Math.Min(A.top, B.top));
			FancyRect R1 = new FancyRect(U.left, U.top, A.left - U.left, A.top - U.top);
			FancyRect R2 = new FancyRect(A.left, U.top, A.right - A.left, A.top - U.top);
			FancyRect R3 = new FancyRect(A.right, U.top, U.right - A.right, A.top - U.top);
			FancyRect R4 = new FancyRect(U.left, A.top, A.left - U.left, A.bottom - A.top);
			FancyRect R5 = new FancyRect(A.right, A.top, U.right - A.right, A.bottom - A.top);
			FancyRect R6 = new FancyRect(U.left, A.bottom, A.left - U.left, U.bottom - A.bottom);
			FancyRect R7 = new FancyRect(A.left, A.bottom, A.right - A.left, U.bottom - A.bottom);
			FancyRect R8 = new FancyRect(A.right, A.bottom, U.right - A.right, U.bottom - A.bottom);

			List<FancyRect> rects = new List<FancyRect>(new FancyRect[] { R1, R2, R3, R4, R5, R6, R7, R8 });
			for (int i = 0; i < rects.Count; i++) {
				if (rects[i].width == 0 || rects[i].height == 0) { rects.RemoveAt(i); i -= 1; }

			}

			return rects;
		}

		public static FancyRect Copy(FancyRect fancyrect) {
			return new FancyRect(fancyrect.x, fancyrect.y, fancyrect.width, fancyrect.height);
		}
		public static FancyRect FromBounds(float x1, float y1, float x2, float y2) {
			Vector2 min = new Vector2(Math.Min(x1, x2), Math.Min(y1, y2));
			Vector2 max = new Vector2(Math.Max(x1, x2), Math.Max(y1, y2));
			return new FancyRect(min.X, min.Y, max.X - min.X, max.Y - min.Y);
		}

        public float centerx {
            get { return this.x + this.width / 2; }
            set { this.x = value - this.width / 2; } 
        }
        public float centery {
            get { return this.y + this.height / 2; }
            set { this.y = value - this.height / 2; }
        }
        public float top {
            get { return this.y; }
            set { this.y = value; }
        }
        public float left {
            get { return this.x; }
            set { this.x = value; }
        }
        public float bottom {
            get { return this.y + this.height; }
            set { this.y = value - this.height; }
        }
        public float right {
            get { return this.x + this.width; }
            set { this.x = value - this.width; }
        }

	}

}
