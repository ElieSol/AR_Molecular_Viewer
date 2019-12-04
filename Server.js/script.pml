cmd.load("file.pdb")
preset.ball_and_stick(selection='all', mode=1)
save Ball_Stick.dae

hide everything, all
show cartoon, all
color purple, ss h
color yellow, ss s
color green, ss ""
save Cartoon.dae
