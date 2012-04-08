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
    int Area2(Point p1,Point p2,Point p3)
    {
    return (p2.getX() - p1.getX()) * (p3.getY() - p1.getY()) - 
	    (p3.getX() - p1.getX()) * (p2.getY() - p1.getY());
    }
    bool between(Point p1,Point p2,Point p3)
    {

    if (! collinear(p1,p2,p3)) return false;
    if (p1.getX() != p2.getX())
	    return ((p1.getX() <= p3.getX()) && (p3.getX() <= p2.getX()) ||
		    ((p1.getX() >= p3.getX()) && (p3.getX() >= p2.getX())));
    else return ((p1.getY() <= p3.getY()) && (p3.getY() <= p2.getY()) ||
		    ((p1.getY() >= p3.getY()) && (p3.getY() >= p2.getY())));
    }
    void clipEar(int i1,Polygon P)
    {
    P.removeVertex(i1);
    }
    bool collinear(Point p1, Point p2, Point p3)
    {
    return Area2(p1,p2,p3) == 0;
    }
    ColorSet color(diagonalSet d,Polygon p)
    {
    ColorSet CSet = new ColorSet(); 
    Edge curDiag = d.getDiagonal(0);
    Point a,b,cut;
    int d1,d2;
    if (p.size == 3)
	    {
	    a=p.getPnt(0);
	    b=p.getPnt(1);
	    cut=p.getPnt(2);
	    a.setColor(0);
	    b.setColor(1);
	    cut.setColor(2);
	    CSet.add(a);
	    CSet.add(b);
	    CSet.add(cut);
	    return CSet;
	    }

    a = p.getPnt(curDiag.getStart().getIndex());
    b = p.getPnt(curDiag.getEnd().getIndex());
    cut = p.getPnt(curDiag.getCutPnt().getIndex());


    p.getPnt(a.getIndex()).setColor(0);
    p.getPnt(b.getIndex()).setColor(1);
    p.getPnt(cut.getIndex()).setColor(2);

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
    int n= P.size;

    for (k=0;k<n;k++) 
      {
      k1 = (k+1) %n;
      if (!((k==i)||(k1==i) || (k==j) || (k1==j)))
	    {
	    if (intersect(P.getPnt(i),P.getPnt(j),P.getPnt(k),P.getPnt(k1)))
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
    public Point getTriangle(int a,int b,Polygon p) {
  
	    for (int i=0;i<p.size;i++)
	      {
	      if ((i!=b) && (i!=a))
		    {
			    if (p.areNeighbors(a,i) && p.areNeighbors(b,i) && (p.getPnt(i).getColor()==-1))
			    {
				    return p.getPnt(i);
			    }
		    }
	     }
       return null;
  
    }
    bool inCone(int i,int j,Polygon P)
    {
    int n = P.size;

    int i1 = (i+1) % n;
    int in1 = (i+n-1)%n;


    if (LeftOn(P.getPnt(in1),P.getPnt(i),P.getPnt(i1)))
       {
       return Left(P.getPnt(i),P.getPnt(j),P.getPnt(in1)) &&
	    Left(P.getPnt(j),P.getPnt(i),P.getPnt(i1));
       }

    else 
       {
       return !(LeftOn(P.getPnt(i),P.getPnt(j),P.getPnt(i1)) && LeftOn(P.getPnt(j),P.getPnt(i),P.getPnt(in1)));
       }
    }
    bool intersect(Point p1,Point p2,Point p3,Point p4)
    {
    if (intersectProp(p1,p2,p3,p4))
       return true;
    else if (between(p1,p2,p3) || between(p1,p2,p4) || between(p3,p4,p1) || between(p3,p4,p2))
       return true;
    else return false;
    }
    bool intersectProp(Point p1,Point p2,Point p3,Point p4)
    {
    if (collinear(p1,p2,p3) || collinear(p1,p2,p4) || collinear(p3,p4,p1) || collinear(p3,p4,p2))
       return false;
    return Xor(Left(p1,p2,p3),Left(p1,p2,p4)) && Xor(Left(p3,p4,p1),Left(p3,p4,p2));
    }
    bool isDiagonal(int i,int j,Polygon P)
    {
    return inCone(i,j,P) && diagonal(i,j,P);
    }
    bool Left(Point p1,Point p2,Point p3)
    {
    return Area2(p1,p2,p3) >0;
    }
    bool LeftOn(Point p1,Point p2,Point p3)
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
    edge curDiag = d.getDiagonal(i);
    Point a,b,cut;
    int d1,d2;

    a = p.getPnt(curDiag.getStart().getIndex());
    b = p.getPnt(curDiag.getEnd().getIndex());
    cut = p.getPnt(curDiag.getCutPnt().getIndex());

    if (cut.getColor() == -1) // point has not been colored
    {
    p.getPnt(cut.getIndex()).setColor(nextColor(a.getColor(),b.getColor()));
    CSet.add(cut);
    if ((d1 = d.isInDiagSet(a,cut)) != -1) CSet.add(recurseColor(d,p,d1));
    if ((d2 = d.isInDiagSet(b,cut)) != -1) CSet.add(recurseColor(d,p,d2));
    }
    else 
    {
    cut = getTriangle(a.getIndex(),b.getIndex(),p);
    if (cut == null) 
      {
      return CSet;
	    }
    p.getPnt(cut.getIndex()).setColor(nextColor(a.getColor(),b.getColor()));
    CSet.add(cut);
    if ((d1 = d.isInDiagSet(a,cut)) != -1) CSet.add(recurseColor(d,p,d1));
    if ((d2 = d.isInDiagSet(b,cut)) != -1) CSet.add(recurseColor(d,p,d2));
    }
    return CSet;
    }
    public diagonalSet triangulate(Polygon P,Graphics g)
    {
    diagonalSet d = new diagonalSet();
    int i,i1,i2;
    int n = P.size;
    if (n>=3)
       for (i=0;i<n;i++)
	    {
	    i1 = (i+1) % n;
	    i2 = (i+2) % n;
	    if (isDiagonal(i,i2,P,g))
	    {
	    d.addDiagonal(P.getPnt(i),P.getPnt(i2),P.getPnt(i1));
	    clipEar(i1,P);
	    return d.merge(triangulate(P,g));
	    }
	    }
    return d;
    }
    boolean Xor(boolean a,boolean b)
    {
    if ((a && b) || (!a && !b)) return false;
    return true;
    }
    }
}
