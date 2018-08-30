package com.ninevastudios.awareness;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Keep;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;

import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import static android.Manifest.permission.ACCESS_FINE_LOCATION;
import static android.support.v4.content.PermissionChecker.PERMISSION_GRANTED;

@Keep
public class PermissionHelperActivity extends Activity {

    private static final int REQUEST_CODE_LOCATION = 101;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        ActivityCompat.requestPermissions(this, new String[]{ACCESS_FINE_LOCATION}, REQUEST_CODE_LOCATION);
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        handleRequestPermissionsResult(this, permissions[0], grantResults[0]);
        finish();
    }

    private void handleRequestPermissionsResult(PermissionHelperActivity activity, String permission, int grantResult) {
        String permissionsResult = serializePermissionResults(activity, permission, grantResult);
        UnityPlayer.UnitySendMessage(AwarenessManager.AWARENESS_SCENE_HELPER, "OnRequestPermissionsResult", permissionsResult);
    }

    public static String serializePermissionResults(Activity activity, String permission, int grantResult) {
        JSONObject json = new JSONObject();
        try {
            json.put("permission", permission);
            json.put("shouldShowRequestPermissionRationale",
                    ActivityCompat.shouldShowRequestPermissionRationale(activity, permission));
            json.put("result", grantResult);
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return json.toString();
    }

    @Keep
    public static void requestLocationPermission(Activity activity) {
        activity.startActivityForResult(new Intent(activity, PermissionHelperActivity.class), REQUEST_CODE_LOCATION);
    }

    @Keep
    public static boolean isLocationPermissionGranted(Context context) {
        return ContextCompat.checkSelfPermission(context, ACCESS_FINE_LOCATION) == PERMISSION_GRANTED;
    }
}
