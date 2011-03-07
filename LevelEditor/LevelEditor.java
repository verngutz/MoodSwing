/**
 * @(#)LevelEditor.java
 *
 *
 * @author Mark Joshua Tan 
 * @version 2.00 4MAR2011
 */
import java.io.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import java.util.*;
import java.awt.image.*;
import javax.imageio.*;
import java.awt.geom.AffineTransform;
import java.lang.Math.*;

public class LevelEditor extends JFrame{
	private int rows, columns;
	private Canvas c;
	private Canvas tool;
	private ScrollPane cpane;
	private JButton kthxbai;
	private PrintStream out;
	private String filename;
	private final int NO_OF_TEXTURES = 10;
    public LevelEditor() {
    	setTitle("MoodSwing Level Editor v2.00");
    	setSize(1024,768);
    	setLayout(null);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		do{
			filename = JOptionPane.showInputDialog("Filename:");
			if(filename == null){
				System.exit(0);
			}
		}while(filename.equals(""));
		try{
			out = new PrintStream(new FileOutputStream(filename+".txt"));
		}catch(Exception e){
			System.exit(1);
		}
    	rows = Integer.parseInt(JOptionPane.showInputDialog("Number of rows:"));
    	columns = Integer.parseInt(JOptionPane.showInputDialog("Number of columns:"));
    	c = new LevelEditorCanvas(rows,columns,NO_OF_TEXTURES,this);
    	tool = new ToolBoxCanvas();
    	cpane = new ScrollPane();
    	kthxbai = new JButton("KTHXBAI");
    	kthxbai.addActionListener(new ActionListener(){
    		public void actionPerformed(ActionEvent ae){
    			int[][] map = ((LevelEditorCanvas) c).getMap();
    			out.println(columns+" "+rows);
    			for(int[] row : map){
    				for(int column : row){
    					out.print(column+" ");
    				}
    				out.println();
    			}
    			System.exit(0);
    		}
    	});
    	cpane.add(c);
    	cpane.setVisible(true);
    	add(kthxbai);
    	add(cpane);
    	add(tool);
    	kthxbai.setBounds(0,0,100,34);
    	cpane.setBounds(0,34,1000,700);
    	tool.setBounds(100,0,331,34);
    	setVisible(true);
    }
    public int getTool(){
    	return ((ToolBoxCanvas) tool).getTool();
    }
    public static void main(String[] args) {
    	new LevelEditor();
    }
}
class LevelEditorCanvas extends Canvas implements MouseListener, MouseMotionListener{
	private Image buffer = null;
	private int[][][] map;
	private BufferedImage[] floors;
	private int rows, columns;
	private LevelEditor levelEditor;
	public LevelEditorCanvas(int r, int c, int noOfTextures, LevelEditor levelEditor){
		map = new int[r][c][2];
		for(int i = 0; i < r; i++){
			for(int j = 0; j < c; j++){
				map[i][j][0] = 4;
			}
		}
		rows = r;
		columns = c;
		this.levelEditor = levelEditor;
		setSize(c*32,r*32);
		setVisible(true);
		floors = new BufferedImage[noOfTextures];
		try{
			for(int i = 0; i < noOfTextures; i++){
				floors[i] = ImageIO.read(getClass().getResource("Floors/"+i+".png"));
			}
		}catch(Exception e){
			System.exit(2);
		}
		addMouseListener(this);
		addMouseMotionListener(this);
		repaint();
	}
	public int[][] getMap(){
		return convertMap(map);
	}
	private int[][] convertMap(int[][][] map){
		int[][] convertedMap = new int[rows][columns];
		for(int i = 0; i < rows; i++){
			for(int j = 0; j < columns; j++){
				switch(map[i][j][0]){
					case 0:
						convertedMap[i][j] = map[i][j][1];
						break;
					case 1:
						convertedMap[i][j] = map[i][j][1] + 100;
						break;
					case 2:
						convertedMap[i][j] = map[i][j][1] + 200;
						break;
					case 3:
						convertedMap[i][j] = map[i][j][1] + 300;
						break;
					case 4:
						convertedMap[i][j] = map[i][j][1] + 10;
						break;
					case 5:
						convertedMap[i][j] = map[i][j][1] + 110;
						break;
					case 6:
						convertedMap[i][j] = map[i][j][1] + 210;
						break;
					case 7:
						convertedMap[i][j] = map[i][j][1] + 1210;
						break;
					case 8:
						convertedMap[i][j] = map[i][j][1] + 310;
						break;
					case 9:
						convertedMap[i][j] = map[i][j][1] + 410;
						break;
					default:
						convertedMap[i][j] = 40;
				}
			}
		}
		return convertedMap;
	}
	public void paint(Graphics gg){
		if(buffer == null){
			buffer = createImage(getWidth(), getHeight());
		}
		Graphics g = buffer.getGraphics();
		g.setColor(Color.GRAY);
		g.fillRect(0,0,getWidth(),getHeight());
		for(int i = 0; i < rows; i++){
			for(int j = 0; j < columns; j++){
				g.drawImage(rotate(floors[map[i][j][0]],map[i][j][1]),j*32,i*32,null);
			}
		}
		g.setColor(Color.BLACK);
		for(int i = 0; i < columns; i++){
			g.drawString(""+i,(i*32)+10,10);
			g.drawString(""+i,(i*32)+10,((rows-1)*32)+30);
		}
		for(int i = 0; i < rows; i++){
			g.drawString(""+i,2,(i*32)+20);
			g.drawString(""+i,((columns-1)*32)+20,(i*32)+20);
		}
		gg.drawImage(buffer, 0, 0, null);
	}
	public void update(Graphics g){
		paint(g);
	}
	public BufferedImage rotate (BufferedImage img, int rotation){
		AffineTransform transformer = new AffineTransform();
	    transformer.rotate(-(Math.toRadians(90*rotation)), img.getWidth()/2, img.getHeight()/2);
	    BufferedImage img2 = new BufferedImage(img.getWidth(), img.getHeight(), BufferedImage.TYPE_INT_RGB);
	    Graphics2D  graphics = (Graphics2D) img2.getGraphics();
	    graphics.drawImage(img, transformer, null);
	    return img2;
	}
    public void mouseClicked(MouseEvent e){
    	int x = e.getY()/32;
    	int y = e.getX()/32;
    	if(e.getButton()==MouseEvent.BUTTON1){
    		map[x][y][0] = levelEditor.getTool();
    		map[x][y][1] = 0;
    	}
    	if(e.getButton()==MouseEvent.BUTTON2){
    		map[x][y][0] = 4;
    		map[x][y][1] = 0;
    	}
    	if(e.getButton()==MouseEvent.BUTTON3){
    		map[x][y][1] = (mod(map[x][y][1]+1,4));
    	}
    	repaint();
    }
    public void mousePressed(MouseEvent e){
    }
    public void mouseReleased(MouseEvent e){
    }
    public void mouseMoved(MouseEvent e){
    }
    public void mouseDragged(MouseEvent e){
    }
    public void mouseEntered(MouseEvent e){
    }
    public void mouseExited(MouseEvent e){
    }
    public int mod(int x, int y){
    	if(x<0){
    		return ((x%y)+y);
    	}
    	return (x%y);
    }
}
class ToolBoxCanvas extends Canvas implements MouseListener, MouseMotionListener{
	private Image buffer;
	private BufferedImage toolbar;
	private int currentTool;
	public ToolBoxCanvas(){
		buffer = null;
		currentTool = 0;
		setVisible(true);
		try{
			toolbar = ImageIO.read(getClass().getResource("Toolbar.png"));
		}catch(Exception e){
			System.exit(2);
		}
		addMouseListener(this);
		addMouseMotionListener(this);
		repaint();
	}
	public void paint(Graphics gg){
		if(buffer == null){
			buffer = createImage(getWidth(), getHeight());
		}
		Graphics g = buffer.getGraphics();
		g.setColor(Color.WHITE);
		g.fillRect(0,0,getWidth(),getHeight());
		
		g.drawImage(toolbar, 0, 0, null);
		g.setColor(Color.RED);
		g.drawString("@", currentTool * 33 + 3, 10);
		
		gg.drawImage(buffer, 0, 0, null);
	}
	public void update(Graphics g){
		paint(g);
	}
	public int getTool(){
		return currentTool;
	}
    public void mouseClicked(MouseEvent e){
    	currentTool = e.getX()/33;
    	repaint();
    }
    public void mousePressed(MouseEvent e){
    }
    public void mouseReleased(MouseEvent e){
    }
    public void mouseMoved(MouseEvent e){
    }
    public void mouseDragged(MouseEvent e){
    }
    public void mouseEntered(MouseEvent e){
    }
    public void mouseExited(MouseEvent e){
    }
    public int mod(int x, int y){
    	if(x<0){
    		return ((x%y)+y);
    	}
    	return (x%y);
    }
}