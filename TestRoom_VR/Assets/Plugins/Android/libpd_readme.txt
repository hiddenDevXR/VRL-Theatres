Included: libpd 0.12.3
Shared object library
Multi-instance support is ON
Utils (required for LibPdIntegration support) are ON
Extras (not required for LibPdIntegration support) are ON
Locale (LC_NUMERIC) support is OFF 

Build steps (performed on Mac OS, but on Windows should be same except for paths):

- Install Android Studio, open Configure->SDK Manager (or Preferences->Appearance and Behavior->System Settings->Android SDK), under "SDK Platforms" install Android 6.0, under "SDK Tools" install NDK (22.0.7026061), CMake (3.10.2.4988404), Platform Tools (29.0.5, newer probably works too), SDK Tools (26.1.1, newer probably works too)

- In git check out git@github.com:libpd/libpd.git, checkout tag 0.12.3, recursively init and update all submodules

- From libpd directory:

mkdir build-android

(export PATH="/Applications/Android Studio.app/Contents/jre/jdk/Contents/Home/bin":~/Library/Android/sdk/platform-tools:$PATH ANDROID_HOME=~/Library/Android/sdk PROJECT=`pwd`; (cd build-android &&
$ANDROID_HOME/cmake/3.10.2.4988404/bin/cmake \
    -DANDROID=1 \
    -DCMAKE_TOOLCHAIN_FILE=$ANDROID_HOME/ndk/22.0.7026061/build/cmake/android.toolchain.cmake \
    -DANDROID_ABI=arm64-v8a \
    -DANDROID_NATIVE_API_LEVEL=26 \
    -DPD_UTILS=ON -DPD_EXTRA=ON -DPD_MULTI=ON -DPD_LOCALE=OFF -DPD_BUILD_C_EXAMPLES=OFF \
    -DCMAKE_BUILD_TYPE=Release \
    ..))

(export PATH="/Applications/Android Studio.app/Contents/jre/jdk/Contents/Home/bin":~/Library/Android/sdk/platform-tools:$PATH ANDROID_HOME=~/Library/Android/sdk; (cd build-android && $ANDROID_HOME/cmake/3.10.2.4988404/bin/cmake --build .))

mv build-android/libs/libpd-multi.so libpd.so
