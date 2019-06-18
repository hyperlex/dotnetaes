with import <nixpkgs> {};

stdenv.mkDerivation {
  name = "dotnetaes";
  buildInputs = [
    mono
    python36Packages.pycryptodome
  ];
  src = ./.;
}
