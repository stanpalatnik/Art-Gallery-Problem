using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeometryTest.Models
{
    class Triangulation {

    public Triangulation(){}
    double Area2(ColoredPoint p1, ColoredPoint p2, ColoredPoint p3)
    {
    return (p2.point.X - p1.point.X) * (p3.point.Y - p1.point.Y) - 
	    (p3.point.X - p1.point.X) * (p2.point.Y - p1.point.Y);
    }

    bool isBetween(ColoredPoint p1,ColoredPoint p2,ColoredPoint p3)
    {
    if (! isCollinear(p1,p2,p3)) return false;
    if (p1.point.X != p2.point.X)
	    return ((p1.point.X <= p3.point.X) && (p3.point.X <= p2.point.X) ||
		    ((p1.point.X >= p3.point.X) && (p3.point.X >= p2.point.X)));
    else return ((p1.point.Y <= p3.point.Y) && (p3.point.Y <= p2.point.Y) ||
		    ((p1.point.Y >= p3.point.Y) && (p3.point.Y >= p2.point.Y)));
    }
    void clipEar(int i1,Polygon P)
    {
    P.removeVertex(i1);
    }

    bool isCollinear(ColoredPoint p1, ColoredPoint p2, ColoredPoint p3)
    {
    return Area2(p1,p2,p3) == 0;
    }

    ColorSet color(DiagonalSet d,Polygon p)
    {
    ColorSet CSet = new ColorSet(); 
    Edge curDiag = d.getDiagonal(0);
    ColoredPoint a,b,cut;
    int d1,d2;
    if (p.vertices.Count == 3)
	    {
	    a=p.getColoredPoint(0);
	    b=p.getColoredPoint(1);
	    cut=p.getColoredPoint(2);
	    a.setColor(0);
	    b.setColor(1);
	    cut.setColor(2);
	    CSet.add(a);
	    CSet.add(b);
	    CSet.add(cut);
	    return CSet;
	    }

    a = p.getColoredPoint(curDiag.getStart().index);
    b = p.getColoredPoint(curDiag.getEnd().index);
    cut = p.getColoredPoint(curDiag.getCutPnt().index);


    p.getColoredPoint(a.getIndex()).setColor(0);
    p.getColoredPoint(b.getIndex()).setColor(1);
    p.getColoredPoint(cut.getIndex()).setColor(2);

    CSet.add(a);
    CSet.add(b);
    CSet.add(cut);


    if ((d1 = d.isInDiagSet(a,cut)) != -1) CSet.add(recurseColor(d,p,d1));
    if ((d2 = d.isInDiagSet(b,cut)) != -1) CSet.add(recurseColor(d,p,d2));

    CSet.add(recurseColor(d,p,0));
    return CSet;

    }
    bool diagonal(int i,int j,Polygon P)
    {
    int k;
    int k1;
    int n= P.vertices.Count;

    for (k=0;k<n;k++) 
      {
      k1 = (k+1) %n;
      if (!((k==i)||(k1==i) || (k==j) || (k1==j)))
	    {
	    if (intersect(P.getColoredPoint(i),P.getColoredPoint(j),P.getColoredPoint(k),P.getColoredPoint(k1)))
	       return false;
	    }
      }
      return true;
    }
    /**
     * This method was created by a SmartGuide.
     * @return Point
     * @param a int
     * @param b int
     */
    public ColoredPoint getTriangle(int a,int b,Polygon p) {
  
	    for (int i=0;i<p.vertices.Count;i++)
	      {
	      if ((i!=b) && (i!=a))
		    {
			    if (p.areNeighbors(a,i) && p.areNeighbors(b,i) && (p.getColoredPoint(i).getColor()==-1))
			    {
				    return p.getColoredPoint(i);
			    }
		    }
	     }
       return null;
  
    }
    bool inCone(int i,int j,Polygon P)
    {
    int n = P.vertices.Count;

    int i1 = (i+1) % n;
    int in1 = (i+n-1)%n;


    if (LeftOn(P.getColoredPoint(in1),P.getColoredPoint(i),P.getColoredPoint(i1)))
       {
       return Left(P.getColoredPoint(i),P.getColoredPoint(j),P.getColoredPoint(in1)) &&
	    Left(P.getColoredPoint(j),P.getColoredPoint(i),P.getColoredPoint(i1));
       }

    else 
       {
       return !(LeftOn(P.getColoredPoint(i),P.getColoredPoint(j),P.getColoredPoint(i1)) && LeftOn(P.getColoredPoint(j),P.getColoredPoint(i),P.getColoredPoint(in1)));
       }
    }
    bool intersect(ColoredPoint p1,ColoredPoint p2,ColoredPoint p3,ColoredPoint p4)
    {
    if (intersectProp(p1,p2,p3,p4))
       return true;
    else if (isBetween(p1,p2,p3) || isBetween(p1,p2,p4) || isBetween(p3,p4,p1) || isBetween(p3,p4,p2))
       return true;
    else return false;
    }
    bool intersectProp(ColoredPoint p1, ColoredPoint p2, ColoredPoint p3, ColoredPoint p4)
    {
    if (isCollinear(p1,p2,p3) || isCollinear(p1,p2,p4) || isCollinear(p3,p4,p1) || isCollinear(p3,p4,p2))
       return false;
    return Xor(Left(p1,p2,p3),Left(p1,p2,p4)) && Xor(Left(p3,p4,p1),Left(p3,p4,p2));
    }
    bool isDiagonal(int i,int j,Polygon P)
    {
    return inCone(i,j,P) && diagonal(i,j,P);
    }
    bool Left(ColoredPoint p1, ColoredPoint p2, ColoredPoint p3)
    {
    return Area2(p1,p2,p3) >0;
    }
    bool LeftOn(ColoredPoint p1, ColoredPoint p2, ColoredPoint p3)
    {
    return Area2(p1,p2,p3) >= 0;
    }
    int nextColor(int c1,int c2)
    {
    if ((c1 + c2) ==1) return 2;
    else if ((c1 + c2) ==2) return 1;
    else return 0;
    }
    ColorSet recurseColor(DiagonalSet d,Polygon p,int i)
    {
    ColorSet CSet = new ColorSet(); 
    Edge curDiag = d.getDiagonal(i);
    ColoredPoint a,b,cut;
    int d1,d2;

    a = p.getColoredPoint(curDiag.getStart().index);
    b = p.getColoredPoint(curDiag.getEnd().index);
    cut = p.getColoredPoint(curDiag.getCutPnt().index);

    if (cut.getColor() == -1) // point has not been colored
    {
        p.getColoredPoint(cut.index).setColor(nextColor(a.index, b.index));
    CSet.add(cut);
    if ((d1 = d.isInDiagSet(a,cut)) != -1) CSet.add(recurseColor(d,p,d1));
    if ((d2 = d.isInDiagSet(b,cut)) != -1) CSet.add(recurseColor(d,p,d2));
    }
    else 
    {
        cut = getTriangle(a.index, b.index, p);
    if (cut == null) 
      {
      return CSet;
	    }
    p.getColoredPoint(cut.index).setColor(nextColor(a.getColor(),b.getColor()));
    CSet.add(cut);
    if ((d1 = d.isInDiagSet(a,cut)) != -1) CSet.add(recurseColor(d,p,d1));
    if ((d2 = d.isInDiagSet(b,cut)) != -1) CSet.add(recurseColor(d,p,d2));
    }
    return CSet;
    }
    public DiagonalSet triangulate(Polygon P,Graphics g)
    {
    DiagonalSet d = new DiagonalSet();
    int i,i1,i2;
    int n = P.vertices.Count;
    if (n>=3)
       for (i=0;i<n;i++)
	    {
	    i1 = (i+1) % n;
	    i2 = (i+2) % n;
	    if (isDiagonal(i,i2,P,g))
	    {
	    d.addDiagonal(P.getColoredPoint(i),P.getColoredPoint(i2),P.getColoredPoint(i1));
	    clipEar(i1,P);
	    return d.merge(triangulate(P,g));
	    }
	    }
    return d;
    }

    bool Xor(bool a,bool b)
    {
    if ((a && b) || (!a && !b)) return false;
    return true;
    }
    }
}
