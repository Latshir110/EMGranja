#include <Keyboard.h>

int maxSensorValue = 0;
int brecha = 500;

void setup() {
  Serial.begin(115200);
  Keyboard.begin();
}

void loop() {
  int sensorValue = analogRead(A0);

  if (sensorValue > brecha) {
    Serial.println(sensorValue);

    Keyboard.press(' ');
    delay(100);
    Keyboard.releaseAll();

    if (sensorValue > maxSensorValue) {
      maxSensorValue = sensorValue;

      Serial.print("Nuevo valor máximo: ");
      Serial.println(maxSensorValue);
    }
  }
}
