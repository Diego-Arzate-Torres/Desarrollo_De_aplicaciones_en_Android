package com.example.actividad1android;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.widget.TextView;
import org.json.JSONException;
import org.json.JSONObject;

public class Actividad1android extends AppCompatActivity {

    TextView EditUsuario = (TextView) findViewById(R.id.EditUsuario);
    TextView EditPassword = (TextView) findViewById(R.id.EditPassword);

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_actividad1android);
    }

    private void realizarPeticionPost() {
        // URL de tu endpoint
        String url = "https://tu-api.com/login";

        // Crear el objeto JSON con los parámetros
        JSONObject parametros = new JSONObject();
        try {
            parametros.put("usuario", EditUsuario.getText().toString());
            parametros.put("contrasena", EditPassword.getText().toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }

        // Crear la solicitud POST
        JsonObjectRequest solicitud = new JsonObjectRequest(Request.Method.POST, url, parametros,
                new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {
                        // Manejar la respuesta JSON
                        try {
                            String mensaje = response.getString("mensaje");
                            //Toast.makeText(getApplicationContext(), "Respuesta: " + mensaje, Toast.LENGTH_LONG).show();
                        } catch (JSONException e) {
                            e.printStackTrace();
                        }
                    }
                },
                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        // Manejar el error
                        //Toast.makeText(getApplicationContext(), "Error: " + error.getMessage(), Toast.LENGTH_LONG).show();
                    }
                });

        // Agregar la solicitud a la cola
        RequestQueue requestQueue = Volley.newRequestQueue(this);
        requestQueue.add(solicitud);
    }
}
