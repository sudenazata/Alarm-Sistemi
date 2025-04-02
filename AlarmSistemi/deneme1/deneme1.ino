#define t_pin 4    // Trigger pin (HC-SR04)
#define e_pin 3    // Echo pin (HC-SR04)
#define b_pin 7    // Buzzer pin

void setup() {
  pinMode(t_pin, OUTPUT);  
  pinMode(b_pin, OUTPUT);  
  pinMode(e_pin, INPUT);    
  Serial.begin(9600);       
}

void loop() {
  digitalWrite(t_pin, LOW);  
  delay(100);                 
  digitalWrite(t_pin, HIGH);  
  delay(10);                   
  digitalWrite(t_pin, LOW);   
  
  long sure = pulseIn(e_pin, HIGH);  
  long mesafe = sure / 58.2;         
  
  Serial.print("Mesafe: ");
  Serial.print(mesafe);
  Serial.println(" cm");

  
  if (mesafe < 50) {
    tone(b_pin, 1000);    
  } else {
    noTone(b_pin);       
  }
  delay(100);  
  
}
