/**
 * @(#)LevelEditor.java
 *
 *
 * @author Mark Joshua Tan 
 * @version 1.00 2011/1/15
 */
import java.io.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import java.util.*;
import java.awt.image.*;
import javax.imageio.*;

public class LevelEditor extends JFrame{
	private int rows, columns;
	private Canvas c;
	private ScrollPane cpane;
	private JButton kthxbai;
	private PrintStream out;
	private String filename;
    public LevelEditor() {
    	setTitle("MoodSwing Level Editor v1.00");
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
    	c = new LevelEditorCanvas(rows,columns);
    	cpane = new ScrollPane();
    	kthxbai = new JButton("KTHXBAI");
    	kthxbai.addActionListener(new ActionListener(){
    		public void actionPerformed(ActionEvent ae){
    			int[][] map = ((LevelEditorCanvas) c).getMap();
    			out.println(rows+" "+columns);
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
    	add(cpane);
    	add(kthxbai);
    	kthxbai.setBounds(0,0,100,20);
    	cpane.setBounds(0,20,1000,700);
    	setVisible(true);
    }
    public static void main(String[] args) {
    	new LevelEditor();
    }
}

class LevelEditorCanvas extends Canvas implements MouseListener, MouseMotionListener{
	private Image buffer = null;
	private int[][] map;
	private BufferedImage[] floors;
	int rows, columns;
	public LevelEditorCanvas(int r, int c){
		setSize(c*32,r*32);
		map = new int[r][c];
		rows = r;
		columns = c;
		setVisible(true);
		floors = new BufferedImage[19];
		try{
			for(int i = 0; i < 19; i++){
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
	private int[][] convertMap(int[][] map){
		int[][] convertedMap = new int[rows][columns];
		for(int i = 0; i < rows; i++){
			for(int j = 0; j < columns; j++){
				switch(map[i][j]){
					case 0:
						convertedMap[i][j] = 0;
						break;
					case 1:
						convertedMap[i][j] = 10;
						break;
					case 2:
						convertedMap[i][j] = 20;
						break;
					case 3:
						convertedMap[i][j] = 1;
						break;
					case 4:
						convertedMap[i][j] = 11;
						break;
					case 5:
						convertedMap[i][j] = 111;
						break;
					case 6:
						convertedMap[i][j] = 211;
						break;
					case 7:
						convertedMap[i][j] = 311;
						break;
					case 8:
						convertedMap[i][j] = 21;
						break;
					case 9:
						convertedMap[i][j] = 1021;
						break;
					case 10:
						convertedMap[i][j] = 2021;
						break;
					case 11:
						convertedMap[i][j] = 3021;
						break;
					case 12:
						convertedMap[i][j] = 121;
						break;
					case 13:
						convertedMap[i][j] = 1121;
						break;
					case 14:
						convertedMap[i][j] = 31;
						break;
					case 15:
						convertedMap[i][j] = 131;
						break;
					case 16:
						convertedMap[i][j] = 231;
						break;
					case 17:
						convertedMap[i][j] = 331;
						break;
					case 18:
						convertedMap[i][j] = 41;
						break;
					default:
						convertedMap[i][j] = 1;
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
				g.drawImage(floors[map[i][j]],j*32,i*32,null);
			}
		}
		
		gg.drawImage(buffer, 0, 0, null);
	}
	public void update(Graphics g){
		paint(g);
	}
    public void mouseClicked(MouseEvent e){
    	int x = e.getY()/32;
    	int y = e.getX()/32;
    	if(e.getButton()==MouseEvent.BUTTON1){
    		map[x][y] = (mod(map[x][y]+1,19));
    	}
    	if(e.getButton()==MouseEvent.BUTTON2){
    		map[x][y] = 0;
    	}
    	if(e.getButton()==MouseEvent.BUTTON3){
    		map[x][y] = (mod(map[x][y]-1,19));
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